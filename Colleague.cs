namespace SecretSantaDraw
{
    internal class Colleague
    {
        private string _name;
        private string _lastName;

        private string _email;
        public string Email { get { return _email; } }

        private Gender _gender;
        private Position _position;
        public Colleague(string name, string surname, Position workBranch, Gender gender)//, string email)
        {
            _name = name;
            _lastName = surname;
            _position = workBranch;
            _gender = gender;
            //_email = email;
        }

        public string GetFullName()
        {
            return _name + " " + _lastName; 
        }
    }
}