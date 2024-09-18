using Newtonsoft.Json;
using System.Text.Json;
using System;

void write(List<Word> palabras, string fileName) {
  string ruta = Directory.GetCurrentDirectory() + $"/{fileName}.json";
  string jsonString = JsonConvert.SerializeObject(palabras);
  File.WriteAllText(ruta, jsonString);
}

List<Word> read(string fileName) {
  string ruta = Directory.GetCurrentDirectory() + $"/{fileName}.json";
  string jsonFromFile = File.ReadAllText(ruta);
  
  return JsonConvert.DeserializeObject<List<Word>>(jsonFromFile)!;
}

List<Word> sortWords(List<Word> palabras) {
  List<Word> auxList = new List<Word>();
  List<string> strings = new List<string>();
  foreach (Word word in palabras) {
    strings.Add(word.name);
  }
  strings.Sort();
  foreach (string word in strings) {
    foreach (Word word1 in palabras) {
      if (word == word1.name) {
        auxList.Add(word1);
      }
    }
  }
  return auxList;
}


void main() {
  Console.WriteLine("Proyecto tabla FAT");
  while (true) {
    List<Word> palabras = read("FAT");
    Console.WriteLine("1 - Crear archivo");
    Console.WriteLine("2 - Listar archivos");
    Console.WriteLine("3 - Abrir un archivo");
    Console.WriteLine("4 - Modificar un archivo");
    Console.WriteLine("5 - Eliminar un archivo");
    Console.WriteLine("6 - Recuperar un archivo");
    Console.WriteLine("7 - Salir");
    string option = Console.ReadLine()!;

    if (option == "1") {
      Console.WriteLine("Ingresar el nombre del archivo: ");
      string word = Console.ReadLine()!;  
      Console.WriteLine($"Ingresar el contenido del archivo: ");
      string def = Console.ReadLine()!;
      DateTime thisDay = DateTime.Today;
      palabras.Add(new Word(word, def, "False", thisDay.ToString("d"), "No existe", "No existe"));
      palabras = sortWords(palabras);
      write(palabras, "FAT");

    } else if (option == "2") {
      int no = 0;
      Console.WriteLine("Listado de todas las palabras: ");
      foreach(Word word in palabras) {
        if (word.papeleria == "False"){
          no += 1;
          Console.WriteLine($"{no}- Nombre: {word.name}\n   Cantidad de caracteres: {(word.definition).Length}\n   Creación: {word.creation}\n   Modificación: {word.modification}");
        }
      }     

    } else if (option == "3") {
      int no = 0;
      Console.WriteLine("Listado de todas las palabras: ");
      foreach(Word word in palabras) {
        if (word.papeleria == "False"){
          no += 1;
          Console.WriteLine($"{no}- Nombre: {word.name}\n   Cantidad de caracteres: {(word.definition).Length}\n   Creación: {word.creation}\n   Modificación: {word.modification}");
        }
      }  
      no = 0;   
      bool second = false;
      Console.WriteLine("Ingrese el numero de archivo: ");
      int buscar = Int32.Parse(Console.ReadLine()!);
      foreach (Word word1 in palabras) {
        if (word1.papeleria == "False"){
          no += 1;
          if (no == buscar){
            Console.WriteLine($"- Nombre: {word1.name}\n  Cantidad de caracteres: {(word1.definition).Length}\n  Creación: {word1.creation}\n  Modificación: {word1.modification}\n  Contenido: {word1.definition}");
            second = true;
          }
        }
      }
      if (!second) { Console.WriteLine("La palabra no existe o fue eliminada. "); }

    } else if (option == "4") {
      int no = 0;
      Console.WriteLine("Listado de todas las palabras: ");
      foreach(Word word in palabras) {
        if (word.papeleria == "False"){
          no += 1;
          Console.WriteLine($"{no}- Nombre: {word.name}\n   Cantidad de caracteres: {(word.definition).Length}\n   Creación: {word.creation}\n   Modificación: {word.modification}");
        }
      }  
      no = 0;   
      bool second = false;
      Console.WriteLine("Ingrese el numero de archivo: ");
      int buscar = Int32.Parse(Console.ReadLine()!);
      List<Word> cont = read("FAT");
      foreach (Word word1 in cont) {
        if (word1.papeleria == "False"){
          no += 1;
          if (no == buscar){
            Console.WriteLine($"- Nombre: {word1.name}\n  Cantidad de caracteres: {(word1.definition).Length}\n  Creación: {word1.creation}\n  Modificación: {word1.modification}\n  Contenido: {word1.definition}");
            second = true;
            Console.WriteLine("Ingrese el nuevo contenido");
            string def = Console.ReadLine()!;
            Console.WriteLine("Confirmación final");
            Console.WriteLine("1. Confirmar");
            Console.WriteLine("2. Calcelar");
            string confirmación = Console.ReadLine()!;
            if (confirmación == "1"){
              DateTime thisDay = DateTime.Today;
              word1.definition = def;
              word1.modification = thisDay.ToString("d");
              Console.WriteLine("El archivo fue editado con exito");
            }
          }
        }
      }
      if (!second) { Console.WriteLine("La palabra no existe o fue eliminada. "); }
      write(cont,"FAT");

    } else if (option == "5") {
      int no = 0;
      Console.WriteLine("Listado de todas las palabras: ");
      foreach(Word word in palabras) {
        if (word.papeleria == "False"){
          no += 1;
          Console.WriteLine($"{no}- Nombre: {word.name}\n   Cantidad de caracteres: {(word.definition).Length}\n   Creación: {word.creation}\n   Modificación: {word.modification}");
        }
      }  
      no = 0;   
      bool found = false;
      List<Word> cont = read("FAT");
      Console.WriteLine("Ingrese el numero de archivo: ");
      int buscar = Int32.Parse(Console.ReadLine()!);
      foreach (Word word1 in cont) {
        if (word1.papeleria == "False"){
          no += 1;
          if (no == buscar){
            Console.WriteLine($"- Nombre: {buscar}\n  Creación: {word1.creation}\n  Modificación: {word1.modification}\n  Contenido: {word1.definition}");
            Console.WriteLine("Confirmación final");
            Console.WriteLine("1. Confirmar");
            Console.WriteLine("2. Calcelar");
            string confirmación = Console.ReadLine()!;
            if (confirmación == "1"){
              DateTime thisDay = DateTime.Today;
              word1.papeleria = "True";
              word1.eliminacion = thisDay.ToString("d");
              Console.WriteLine("El archivo fue eliminado con exito");
            }
          }
        }
        found = true;
      }
      if (!found) { Console.WriteLine("El archivo no existe o fue eliminado. "); }
      write(cont,"FAT");
    } else if (option == "6") {
      int no = 0;
      Console.WriteLine("Listado de todas las palabras: ");
      foreach(Word word in palabras) {
        if (word.papeleria == "True"){
          no += 1;
          Console.WriteLine($"{no}- Nombre: {word.name}\n   Cantidad de caracteres: {(word.definition).Length}\n   Creación: {word.creation}\n   Modificación: {word.modification}");
        }
      }     
      no = 0;   
      bool second = false;
      Console.WriteLine("Ingrese el numero de archivo: ");
      int buscar = Int32.Parse(Console.ReadLine()!);
      List<Word> cont = read("FAT");
      foreach (Word word1 in cont) {
        if (word1.papeleria == "True"){
          no += 1;
          if (no == buscar){
            second = true;
            Console.WriteLine("Confirmación final");
            Console.WriteLine("1. Confirmar");
            Console.WriteLine("2. Calcelar");
            string confirmación = Console.ReadLine()!;
            if (confirmación == "1"){
              word1.papeleria = "False";
              word1.eliminacion = "No existe";
              Console.WriteLine("El archivo fue restaurado con exito");
            }
          }
        }
      }
      if (!second) { Console.WriteLine("El archivo no existe. "); }
      write(cont,"FAT");
    } else if (option == "7") {
      break;
    }
  }
}

main();

class Word {
  public string name;
  public string definition;
  public string papeleria;
  public string creation;
  public string modification;
  public string eliminacion;
  public Word(string name, string definition, string papeleria, string creation, string modification, string eliminacion) {
    this.name = name;
    this.definition = definition;
    this.papeleria = papeleria;
    this.creation = creation;
    this.modification = modification;
    this.eliminacion = eliminacion;
  }
}
