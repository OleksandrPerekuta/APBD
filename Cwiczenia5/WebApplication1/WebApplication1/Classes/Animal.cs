namespace WebApplication1.Classes;

public class Animal
{
    private static int _nextId = 0;
    public int Id { get; internal set; } = _nextId++; 
    public string Name { get; set; }
    public string Kategoria { get; set; }
    public float Weight { get; set; }
    public string WoolColor { get; set; }
    private ICollection<Appointment> _appointments = new List<Appointment>();

    public void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
    }

    public ICollection<Appointment> GetAppointments()
    {
        return _appointments;
    } 

    public void SetId(int id)
    {
        Id = id;
        _nextId--;
    }

}