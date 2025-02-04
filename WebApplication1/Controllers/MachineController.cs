using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApplication1.Models;

public class MachineController : Controller
{
    private readonly string _filePath = "wwwroot/data/machines.json";

    private List<Machine> ChargerMachines()
    {
        var json = System.IO.File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Machine>>(json) ?? new List<Machine>();
    }

    private void SauvegarderMachines(List<Machine> machines)
    {
        var json = JsonSerializer.Serialize(machines, new JsonSerializerOptions { WriteIndented = true });
        System.IO.File.WriteAllText(_filePath, json);
    }

    public IActionResult Index()
    {
        var machines = ChargerMachines();
        return View(machines);
    }

    [HttpPost]
    public IActionResult ModifierPoids(int id, decimal delta)
    {
        var machines = ChargerMachines();
        var machine = machines.FirstOrDefault(m => m.Id == id);

        if (machine != null)
        {
            machine.PoidsActuel += delta;
            SauvegarderMachines(machines);
        }

        return RedirectToAction("Index");
    }
    
    [HttpPost]
    public IActionResult MettreAJourPoids(int id, decimal poids)
    {
        var machines = ChargerMachines();
        var machine = machines.FirstOrDefault(m => m.Id == id);

        if (machine != null)
        {
            machine.PoidsActuel = poids;
            SauvegarderMachines(machines);
        }

        return RedirectToAction("Index");
    }
    
    [HttpGet]
    public IActionResult Ajouter()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Ajouter(Machine machine, IFormFile imageFile)
    {
        if (imageFile == null || !imageFile.ContentType.Contains("image/jpeg"))
        {
            ModelState.AddModelError("Image", "Veuillez uploader un fichier JPG uniquement.");
            return View();
        }

        var imagePath = Path.Combine("wwwroot/images", $"imageId{machine.Id}.jpg");
        using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            imageFile.CopyTo(stream);
        }

        var machines = ChargerMachines();

        machines.Add(machine);

        SauvegarderMachines(machines);

        return RedirectToAction("Index", "Machine");
    }


}