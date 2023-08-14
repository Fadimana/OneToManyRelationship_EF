using Microsoft.EntityFrameworkCore;


OneToManyDbContext context = new OneToManyDbContext();
Departman1 departman1 = new()
{
    DepartmanName = "Yazılım",
    Calisanlar1 = new HashSet<Calisan1>() { new() { CalisanName = "Fadimana Dikici" }, new() { CalisanName = "Esma Aksan " } }
};

await context.AddAsync(departman1);
await context.SaveChangesAsync();

Departman1 departman2 = new();
departman2.Calisanlar1 = new HashSet<Calisan1>();
departman2.DepartmanName = "Müşavir";
departman2.Calisanlar1.Add(new() { CalisanName = "Sümeyra DİKİCİ" });
departman2.Calisanlar1.Add(new() { CalisanName = "Muhammet Güneri" });
departman2.Calisanlar1.Add(new() { CalisanName = "Fatmanur Güneri" });

await context.AddAsync(departman2);
await context.SaveChangesAsync();


//principal üzerinden güncelleme işlemi

Departman1 departman3 = await context.Departmanlar1
                            .Include(b => b.Calisanlar1)
                            .FirstOrDefaultAsync(b => b.Id == 2);

Calisan1 silinecekCalisan = departman3.Calisanlar1.FirstOrDefault(p => p.Id == 5);
departman3.Calisanlar1.Remove(silinecekCalisan);

departman3.Calisanlar1.Add(new() { CalisanName = "Fatma " });

await context.SaveChangesAsync();

//depented üzerinden update
Calisan1 calisan1 = await context.Calisanlar1.FindAsync(4);
Departman1 dep = await context.Departmanlar1.FindAsync(1);
calisan1.Departman = dep;

await context.SaveChangesAsync();

//silme işlemi
Departman1 departman4 =await context.Departmanlar1.Include(b=>b.Calisanlar1)
            .FirstOrDefaultAsync(b=>b.Id==1);
Calisan1 calisan = departman1.Calisanlar1.FirstOrDefault(p => p.Id == 2);

context.Calisanlar1.Remove(calisan);
await context.SaveChangesAsync();


class Calisan1 //dependent
{
    public int Id { get; set; }
    public string? CalisanName { get; set; }

    public Departman1? Departman { get; set; }
}

class Departman1 //principal
{
    public Departman1()
    {
        Calisanlar1 = new HashSet<Calisan1>();
    }
    public int Id { get; set; }
    public string? DepartmanName { get; set; }
    
    public ICollection<Calisan1>? Calisanlar1 { get; set; }

}


class OneToManyDbContext:DbContext
{
    public DbSet<Calisan1>Calisanlar1 { get; set; }
    public DbSet<Departman1>Departmanlar1 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }
}