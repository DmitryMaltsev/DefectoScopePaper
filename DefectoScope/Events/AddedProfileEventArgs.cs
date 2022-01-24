namespace DefectoScope
{
    public class AddedProfileEventArgs
    {
        /// <summary>
        /// Добавленный профиль
        /// </summary>
        public byte[] Profile { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="profile">Добавленный профиль</param>
        internal AddedProfileEventArgs(byte[] profile) => Profile = profile;
    }


}