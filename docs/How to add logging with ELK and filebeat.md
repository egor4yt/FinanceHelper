# How to add logging with ELK and filebeat

1. Add ELK environment variables to your .env file. You can find example in [.env](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%20%2B%20ELK%2F.env)
2. Go to the './src' directory with ``cd src`` and create 'ELK-config' directory
3. Go to the './src' directory with ``cd ELK-config``
4. Create your version or copy example of the [filebeat.yml](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%20%2B%20ELK%2FELK-config%2Ffilebeat.yml) in 'src/ELK-config' directory (it is required to be read-only)
5. Create your version or copy example of the [logstash.conf](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%20%2B%20ELK%2FELK-config%2Flogstash.conf) in 'src/ELK-config' directory (it is required to be read-only)
6. Add some services to docker-compose file (elk-setup, elk-es01, elk-kibana, elk-logstash, filebeat). You can find example in [docker-compose.yaml](Docker%20examples%2FApplication%20%2B%20Database%20%2B%20Telegram%20bot%20%2B%20Kafka%20%2B%20ELK%2Fdocker-compose.yaml)

<!--Environment-->
## Description of ELK variables

| Variable             | Description                                     |
|:---------------------|:------------------------------------------------|
| ELK_VERSION          | Version of elastic products                     |
| ELK_ELASTIC_PASSWORD | Password of the 'elastic' user in elasticsearch |
| ELK_ELASTIC_PORT     | Elasticsearch port to access from local network |
| ELK_KIBANA_PASSWORD  | Password of the 'kibana_system' user in kibana  |
| ELK_KIBANA_PORT      | Kibana port to access from local network        |
| ELK_ENCRYPTION_KEY   | Kibana encryption key                           |
