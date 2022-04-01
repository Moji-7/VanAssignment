
namespace TransferApi.Infrastructure.ExceptionHandling
{
    [Serializable]
    class InvalidTransferDescriptionException : Exception
    {
        private const string DefaultMessage = "Entity does not exist.";
        public InvalidTransferDescriptionException() : base(DefaultMessage) {

       
         }

        public InvalidTransferDescriptionException(string name) : base(String.Format("Invalid transfer description : {0}", name))
        {

        }
        public InvalidTransferDescriptionException(Guid transferId) : base(String.Format("cant not save transfer to database : {0}", transferId.ToString()))
        {

        }
    }
}