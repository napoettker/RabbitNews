# RabbitNews

Basic implementation of the messaging pattern with rabbitmq. This application is based on asp.net core and contains two Microservices, a simple ddd design, the cqrs pattern with mediatR, a mongodb database and will be deployed with docker.

### Microservice NewsGenerator:
  Simple Console Application, which publishes random news objects to the rabbitmq queue.

### Microservice RabbitNews.Web:
  Simple asp.net core razor pages application, which consumes the data from the rabbitmq queue by a background task, saves it to a mongodb database and shows all news objects on     one page.

## Setup
  -$ cd .\RabbitNews\RabbitNewsApp \
  -$ docker-compose
