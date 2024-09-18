using Newtonsoft.Json;
using System.Text.Json;

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
      Console.WriteLine("Escriba la palabra: ");
      string word = Console.ReadLine()!;  
      Console.WriteLine($"Escriba la definición: ");
      string def = Console.ReadLine()!;
      Console.WriteLine($"Ingrese la fecha: ");
      string fech = Console.ReadLine()!;
      palabras.Add(new Word(word, def, "False", fech, "No existe", "No existe"));
      palabras = sortWords(palabras);
      write(palabras, "FAT");

    } else if (option == "2") {
      int no = 0;
      Console.WriteLine("Listado de todas las palabras: ");
      foreach(Word word in palabras) {
        if (word.papeleria == "False"){
          no += 1;
          Console.WriteLine($"{no}- Nombre: {word.name}\n  Creación: {word.creation}\n  Modificación: {word.modification}");
        }
      }     

    } else if (option == "3") {
      bool found = false;
      bool second = false;
      Console.WriteLine("Escriba la palabra: ");
      string buscar = Console.ReadLine()!;
      foreach (Word word1 in palabras) {
        if (word1.name == buscar) {
            if (word1.papeleria == "False"){
                Console.WriteLine($"- Nombre: {buscar}\n  Creación: {word1.creation}\n  Modificación: {word1.modification}\n  Contenido: {word1.definition}");
                second = true;
            }
          found = true;
        }
      }
      if (!found) { Console.WriteLine("La palabra no ha sido encontrada. "); }
      if (!second) { Console.WriteLine("La palabra fue eliminada. "); }

    } else if (option == "4") {
      bool found = false;
      Console.WriteLine("Escriba la palabra: ");
      string word = Console.ReadLine()!;
      Word? foundWord = null;
       foreach (Word word1 in palabras) {
        if (word1.name == word) {
          foundWord = word1;
          found = true;
        }
      }
      if (!found) { Console.WriteLine("La palabra no ha sido encontrada. "); } else {
        palabras.Remove(foundWord!);
      }

    } else if (option == "5") {
      bool found = false;
      bool second = false;
      List<Word> cont = read("FAT");
      Console.WriteLine("Escriba la palabra: ");
      string buscar = Console.ReadLine()!;
      foreach (Word word1 in cont) {
        if (word1.name == buscar) {
            if (word1.papeleria == "False"){
                Console.WriteLine($"- Nombre: {buscar}\n  Creación: {word1.creation}\n  Modificación: {word1.modification}\n  Contenido: {word1.definition}");
                second = true;
                Console.WriteLine("Confirmación final");
                Console.WriteLine("1. Confirmar");
                Console.WriteLine("2. Calcelar");
                string confirmación = Console.ReadLine()!;
                if (confirmación == "1"){
                  Console.WriteLine("Ingrese la fecha actual");
                  string papel = Console.ReadLine()!;
                  word1.papeleria = "True";
                  word1.eliminacion = papel;
                }
            }
          found = true;
        }
      }
      if (!found) { Console.WriteLine("La palabra no ha sido encontrada. "); }
      if (!second) { Console.WriteLine("La palabra fue eliminada. "); }
      write(cont,"FAT");
    } else if (option == "6") {
      Console.WriteLine("LISTADO DE PALABRAS ELIMINADAS: ");

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
