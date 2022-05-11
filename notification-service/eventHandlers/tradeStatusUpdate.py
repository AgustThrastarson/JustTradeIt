import pika
from services import emailService
from connection import queueConnection
from time import sleep

def setup_handler(channel, exchange_name):

    create_order_routing_key = 'update_trade_request'
    update_trade_queue = 'update_trade_queue'

    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)
    channel.queue_declare(queue=update_trade_queue, durable=True)
    channel.queue_bind(exchange=exchange_name, queue=update_trade_queue, routing_key=create_order_routing_key)
    print(' [*] Waiting for request. To exit press CTRL+C')


    def callback(ch, method, properties, body):
        emailService.send_trade_update_email(ch, method, properties, body)

    channel.basic_consume(queue=update_trade_queue, auto_ack=True, on_message_callback=callback)


