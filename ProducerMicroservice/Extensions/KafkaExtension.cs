using Confluent.Kafka;

namespace ProducerMicroservice.Extensions
{
    public static class KafkaExtension
    {

        public static IServiceCollection KafkaConfiguration(this IServiceCollection services, IConfiguration config)
        {
            var producerConfig = new ProducerConfig();
            config.Bind("producer", producerConfig);

            services.AddSingleton<ProducerConfig>(producerConfig);

            return services;
        }
    }
}
