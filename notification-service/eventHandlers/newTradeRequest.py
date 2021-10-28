import pika
def setup_handler(channel, exchange_name):
    connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()

    channel.exchange_declare(exchange=exchange_name, exchange_type='direct', durable=True)

    result = channel.queue_declare(queue='new-trade-queue', exclusive=True)
    queue_name = result.method.queue

    channel.queue_bind(exchange=exchange_name, queue=queue_name, routing_key="new_trade_request")

    print(' [*] Waiting for logs. To exit press CTRL+C')

    def callback(ch, method, properties, body):
        print(" [x] %r" % body)

    channel.basic_publish(exchange=exchange_name,
                      routing_key="new-trade-request",
                      body='message',
                      properties=pika.BasicProperties(
                         delivery_mode = 2, # make message persistent
                      ))

    channel.basic_consume(
        queue=queue_name, consumer_callback=callback, no_ack=True)

    channel.start_consuming()


