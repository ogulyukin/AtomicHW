namespace Core
{
    public interface ICharacter
    {
        public AtomicEvent<int> OnTakeDamage { get;}
    }
}
