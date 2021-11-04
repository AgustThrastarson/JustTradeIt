FROM python:latest
LABEL maintainer="Agust Thor Thrastarson"

COPY . /app
WORKDIR /app

RUN pip install -r requirements.txt

CMD [ "python3", "run.py" ]