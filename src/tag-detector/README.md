Tag Detector
=========================
Librería para la detección de múltiples etiquetas QR (aunque también detecta otros tipos) sobre una imagen. Se apoya en Open CV para realizar un tratamiento previo de la imagen y en la librería ZBar para la detección de las etiquetas.

## Source code

La implementacion se realiza en .NET. El código fuente se encuentra en la carpeta `src`.

## Instalación

La librería se debe añadir como una referencia en el proyecto en el que se desee utilizar, de la misma forma que se añade cualquier otra dll en un proyecto .NET.

## Uso

La librería únicamente contiene una clase _Detector_ la cual sólo tiene un método estático _detectTags_. A continuación se incluye un ejemplo de uso:

```cs
string filename = "qrTest.jpg";
Image input = Image.FromFile(filename);
List<Tag> result = Detector.detectTags(input, showProcessedInput: false);
```

Cada objeto Tag contiene, entre otra información, la siguiente:
* __Data__: array de bytes con el contenido de la etiqueta.
* __Polygon__: lista de puntos con la localización de la etiqueta.

## Contributing & License

Adrián Alonso González