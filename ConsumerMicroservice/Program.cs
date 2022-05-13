using Confluent.Kafka;

string TopicName = "dotin";

var config = new ConsumerConfig
{
    GroupId = "gid-consumers",
    BootstrapServers = "localhost:9092",
};

Console.WriteLine("listen to dotin topic ...");

using (var consumer = new ConsumerBuilder<string, string>(config).Build())
{
    consumer.Subscribe(TopicName);

    //TopicPartition partition = new TopicPartition(TopicName, new Partition(2));
    //consumer.Assign(partition);

    while (true)
    {
        var cr = consumer.Consume();
        Console.WriteLine("================New Message==================");
        Console.WriteLine("Key = " + cr.Message.Key);
        Console.WriteLine("Value = " + cr.Message.Value);
        Console.WriteLine("=============================================");
    }
}