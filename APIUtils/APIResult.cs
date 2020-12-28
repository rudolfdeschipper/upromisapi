namespace APIUtils.APIMessaging
{
    public class APIResult<T>
    {
        public int ID { get; set; }
        public T DataSubject { get; set; }
        public string Message { get; set; }
    }
}
