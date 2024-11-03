# How to start application with telegram bot, database and kafka in docker

1. Go to the 'src' directory with ``cd src``

2. Create your version or copy example of [.env](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%2F.env) the in ./src directory

3. Create your version or copy example of the [docker-compose.yaml](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%2Fdocker-compose.yaml) in ./src directory

4. Execute command to start application ``docker compose up -d --build --force-recreate``

5. If you used example of '.env' and 'docker-compose.yaml' files, application will be available at http://localhost:5719/swagger or http://127.0.0.1:5719/swagger.

<!--Environment-->
## Environment configuration

Before you start application you need to create environment variables file.

Environment variables is variables which application using while working. Environment variables file must be located in the same directory with the docker compose file and have name '.env'.

### Description of all variables

| Variable                 | Description                                      |
|:-------------------------|:-------------------------------------------------|
| DB_NAME                  | Database name                                    |
| DB_USER                  | Database default user name                       |
| DB_PASSWORD              | Database default user password                   |
| DB_PUBLIC_PORT           | Database port to access from local network       |
| WEBAPI_PUBLIC_PORT       | Port to access to Application from local network |
| CORS_ORIGINS             | Allowed origin for CORS requests                 |
| TELEGRAM_BOT_PUBLIC_PORT | Port to access to Bot API from local network     |
| TELEGRAM_BOT_WEBHOOK_URL | Your telegram webhook url                        |
| TELEGRAM_BOT_API_KEY     | Your telegram bot API key                        |
