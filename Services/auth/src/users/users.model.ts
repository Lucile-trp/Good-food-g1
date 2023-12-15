import { Schema } from 'mongoose';

export const UserSchema = new Schema({
  email: { type: String, required: true },
  password: { type: String, required: true },
});

export class User {
  constructor(
    public id: string,
    public email: string,
    public password: string,
  ) {}
}
