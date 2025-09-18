import { Deserializable } from '../interfaces/deserializable';

export class AlimentoBebidaDTO implements Deserializable {
  id: number;
  nombre: string;
  descripcion: string | null;
  estatus: boolean;
  constructor() {
    this.id = 0;
    this.nombre = '';
    this.descripcion = null;
    this.estatus = true;
  }
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
