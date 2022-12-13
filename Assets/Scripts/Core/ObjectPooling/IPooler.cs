namespace JellyButton.Exam.Core.ObjectPooling
{
    /// <summary>
    /// Supposed to be used on object pool managers, if needed.
    /// Responsible for providing "outsiders" a way to release objects back to the pool.
    /// </summary>
    public interface IPooler
    {
        public void Release(Pooled obj);
    }
}