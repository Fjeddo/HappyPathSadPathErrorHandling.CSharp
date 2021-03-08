namespace AfterRefactor.Services
{
    public class ServiceResult<T>
    {
        public T Data { get;  }
        public int State { get;  }

        public ServiceResult(int state, T data)
        {
            Data = data;
            State = state;
        }
    }
}
