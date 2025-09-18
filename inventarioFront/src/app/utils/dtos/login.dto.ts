import { Deserializable } from '../interfaces/deserializable';

export class LoginDTO implements Deserializable {
  login: string;
  contrasenia: string;
  constructor() {
    this.login = '';
    this.contrasenia = '';
  }
  deserialize(input: any): this {
    Object.assign(this, input);
    return this;
  }
}
