import pika
import requests
import json

connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
# bý til channel
channel = connection.channel()
# stilla upp hvað þessi exhange heitir sem við erum að fara nota
exchange_name = 'order_exchange'
create_order_routing_key = 'create_order'
# Sets up a queue called email-queue 
email_queue_name = 'email_queue'

# output_routing_key = 'order_success'

# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
# Declare the queue, if it doesn't exist
channel.queue_declare(queue=email_queue_name, durable=True)
# Bind the queue to a specific exchange with a routing key
# Sets up a queue called email-queue which is bound to the create-order routing key
channel.queue_bind(queue=email_queue_name, exchange=exchange_name, routing_key=create_order_routing_key)

email_template = '''<h2>Thank you for ordering!</h2><p>We hope you will enjoy our lovely product.</p><table><thead><tr style="background-color: rgba(155, 155, 155, .2)">
<th>Description</th><th>Unit price</th><th>Quantity</th><th>Row price</th></tr></thead><tbody>%s</tbody></table>'''


# Done að setja inn mailgun info
def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/sandboxc39b6a2572af4dcc97cddbfd1c593e87.mailgun.org/messages",
        auth=("api", "d04bdf42335692a23d7650fa336f4809-53c13666-723b5036"),
        data={"from": "Mailgun Sandbox <postmaster@sandboxc39b6a2572af4dcc97cddbfd1c593e87.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})


def send_order_email(ch, method, properties, data):
    parsed_msg = json.loads(data)

    # email = parsed_msg['email']
    items = parsed_msg['items']
    
    info_html = ''.join(['<tr><td>%s</td><td>%d</td><td>%d</td><td>%d</td></tr>' % (item['description'],
                                                                                     item['unitPrice'], item['quantity'],
                                                                                     int(item['quantity'])
                                                                                     * int(item['unitPrice']))
                          for item in items])
    representation = email_template % info_html
    send_simple_message(parsed_msg['email'], 'Successful order!', representation)

channel.basic_consume(on_message_callback=send_order_email, queue=email_queue_name, auto_ack=True)

channel.start_consuming()
connection.close()