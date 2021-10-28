import pika
import requests
import json

connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
channel = connection.channel()
exchange_name = 'order_exchange'
create_order_routing_key = 'create_order'
email_queue_name = 'email_queue'
email_template = '<h2>Thank you for ordering @ Cactus heaven!</h2><p>We hope you will enjoy our lovely product and don\'t hesitate to contact us if there are any questions.</p><table><thead><tr style="background-color: rgba(155, 155, 155, .2)"><th>Description</th><th>Unit price</th><th>Quantity</th><th>Row price</th></tr></thead><tbody>%s</tbody></table>'

# Declare the exchange, if it doesn't exist
channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
# Declare the queue, if it doesn't exist
channel.queue_declare(queue=email_queue_name, durable=True)
# Bind the queue to a specific exchange with a routing key
channel.queue_bind(exchange=exchange_name, queue=email_queue_name, routing_key=create_order_routing_key)

def send_simple_message(to, subject, body):
    return requests.post(
        "https://api.mailgun.net/v3/sandbox39f0513cae5c403392c92c8229289342.mailgun.org/messages",
        auth=("api", "f623bcbd0ec833f0552f28201735adf9-2ac825a1-8caad992"),
        data={"from": "Mailgun Sandbox <postmaster@sandbox39f0513cae5c403392c92c8229289342.mailgun.org>",
              "to": to,
              "subject": subject,
              "html": body})

def send_order_email(ch, method, properties, data):
    parsed_msg = json.loads(data)
    email = parsed_msg['email']
    items = parsed_msg['items']
    items_html = ''.join([ '<tr><td>%s</td><td>%d</td><td>%d</td><td>%d</td></tr>' % (item['description'], item['unitPrice'], item['quantity'], int(item['quantity']) * int(item['unitPrice'])) for item in items ])
    representation = email_template % items_html
    send_simple_message(parsed_msg['email'], 'Successful order!', representation)

channel.basic_consume(send_order_email,
                      queue=email_queue_name,
                      no_ack=True)

channel.start_consuming()
connection.close()