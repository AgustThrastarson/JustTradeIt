from connection.queueConnection import setup
from eventHandlers import newTradeRequest, tradeStatusUpdate

channel, connection, exchange_name = setup()

newTradeRequest.setup_handler(channel, exchange_name)
tradeStatusUpdate.setup_handler(channel, exchange_name)

channel.start_consuming()
connection.close()
