import pika
from services import emailService
from connection import queueConnection
from time import sleep

def setup_handler(channel, exchange_name):

    #channel, connection, exchange_name = queueConnection.setup()
    create_order_routing_key = 'update_trade_request'
    update_trade_queue = 'update_trade_queue'

    # Declare the exchange, if it doesn't exist
    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
    # Declare the queue, if it doesn't exist
    channel.queue_declare(queue=update_trade_queue, durable=True)
    # Bind the queue to a specific exchange with a routing key
    channel.queue_bind(exchange=exchange_name, queue=update_trade_queue, routing_key=create_order_routing_key)
    print(' [*] Waiting for request. To exit press CTRL+C')


    def callback(ch, method, properties, body):
        emailService.send_trade_update_email(ch, method, properties, body)
        print('ayo')

    channel.basic_consume(queue=update_trade_queue, auto_ack=True, on_message_callback=callback)


