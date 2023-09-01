namespace MotorMax.Servicios
{
    public class PaymentResult
    {
        public PaymentResult(string transaccionId, bool exitoso, string mensaje)
        {
            TransaccionId = transaccionId;
            Exitoso = exitoso;
            Mensaje = mensaje;
        }

        //public string TransaccionId { get; private set; }
        public string TransaccionId { get; set; }

        //public bool Exitoso { get; private set; }
        public bool Exitoso { get; set; }
        public string Mensaje { get; private set; }
    }
}
