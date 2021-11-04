import pika
from connection.queueConnection import setup
from eventHandlers import tradeStatusUpdate, newTradeRequest

channel, connection, exchange_name = setup()

tradeStatusUpdate.setup_handler(channel, exchange_name)
newTradeRequest.setup_handler(channel, exchange_name)

channel.start_consuming()
connection.close()
