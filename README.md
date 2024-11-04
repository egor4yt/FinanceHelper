# About project

This project is an open-source backend application designed to help with the finance management of an ordinary person.
You can create expense items, income sources, and finance distribution plans, as well as watch dashboards for your expenses.
Also, you can interact with your data using a telegram bot.
The application supports localization and globalization.


# Structure of the repository

- src - source code of the backend of this application
- docs - project documentation

# Stack

Technologies used in application development

## Backend
- .NET 8.0
- Entity Framework (ORM)
- MediatR (helps to organize CQRS pattern)
- FluentValidation (helps to validate objects)
- Serilog (logging)
- Swagger (API documentation)
- XUnit (unit testing)
- Confluent.Kafka (message broker)
- Telegram.Bot (telegram integration)

## Data
- PostgreSQL (OLTP database)
- Kafka (message broker)
- Zookeeper (distributed applications configs)
- Elasticsearch (logs analysis engine)

## Third-party integration
- Filebeat (logs collector)
- Logstash (logs processor)
- Kibana (logs visualization)
- Telegram bot (messenger)
- Telegram mini app (react webb application - [GitHub](https://github.com/officer04/finance-helper))

# Infrastructure of the FinanceHelper

<img alt="Infrastructure" src="docs%2FImages%2FInfrastructure.svg" title="Infrastructure"/>

# Contacts
You can contact me at:
- Telegram: [@egor4yt](https://t.me/egor4yt)
- Gmail: [egor4yt@gmail.com](mailto:egor4yt@gmail.com)
