import { Pipe, PipeTransform } from "@angular/core";

@Pipe({name: 'textToUrl'})
export class TextToUrlPipe implements PipeTransform {

  public transform(value: string, exponent = 1): string {
    return value.toLowerCase()
      .normalize("NFD")
      .replace(/ - /g, '_-_')
      .replace(/ /g, '-')
      .replace(/[^-_a-zA-Zs0-9]/g, "");
  }
}
