# Notification service
This service is a crucial part of the microservice structure, because it emits notifications to users regarding a trade. There are two notifications which are emitted:

 - A new trade request has arrived
 - An active trade request changes its status

## Edit before running

In connection/queueConnection.py you should change the following values:
 - \<name-of-exchange>
 - \<username>
 - \<password>

## Running the project
This project is **Python** project and therefore you need to have **Python** installed on your computer ([Python installation](https://www.python.org/downloads/)). You can choose to either create a virtual environment or just run your global **Python** packages, but there is a *requirements.txt* file in the root of the project folder, which contains a list of all the required dependencies. Run it by issuing the following command:

    pip install -r requirements.txt

After that you can run the project by issuing the following command:

    python ./services.py

You will need to implement the code marked with a comment section like this:

    def send_simple_message(to, subject, body):
      # TODO: Implement

The structure of the project should be self-explanatory, and you will only be working in the following files:

 - connection/queueConnection.py
 - eventHandlers/newTradeRequest.py
 - eventHandlers/tradeStatusUpdate.py
 - services/emailService.py

All other files should be unchanged.