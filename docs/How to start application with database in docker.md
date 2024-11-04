# How to start the application with telegram bot, database, and Kafka in docker

1. Go to the 'src' directory with ``cd src``
2. Create your version or copy the example of [.env](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%2F.env) the in ./src directory
3. Create your version or copy the example of the [docker-compose.yaml](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%2Fdocker-compose.yaml) in ./src directory
4. Execute command to start the application ``docker compose up -d --build --force-recreate``
5. If you used the example of '.env' and 'docker-compose.yaml' files, the application will be available at http://localhost:5719/swagger or http://127.0.0.1:5719/swagger.

<!--Environment-->
## Environment configuration

Before you start the application you need to create an environment variables file.

Environment variables are variables that the applications use while working. The Environment variables file must be located in the same directory as the docker-compose file and have the name '.env'.

### Description of all variables

| Variable                 | Description                                          |
|:-------------------------|:-----------------------------------------------------|
| DB_NAME                  | Database name                                        |
| DB_USER                  | Database default user name                           |
| DB_PASSWORD              | Database default user password                       |
| DB_PUBLIC_PORT           | Database port to access from local network           |
| WEBAPI_PUBLIC_PORT       | Port to access to Application from the local network |
| CORS_ORIGINS             | Allowed origin for CORS requests                     |
| TELEGRAM_BOT_PUBLIC_PORT | Port to access to Bot API from the local network     |
| TELEGRAM_BOT_WEBHOOK_URL | Your telegram webhook url                            |
| TELEGRAM_BOT_API_KEY     | Your telegram bot API key                            |
