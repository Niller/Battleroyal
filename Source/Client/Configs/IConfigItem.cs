namespace Client.Configs
{
    public interface IConfigItem<out TKey, out TValue>
    {
        TKey GetKey();
        TValue GetValue();
    }
}