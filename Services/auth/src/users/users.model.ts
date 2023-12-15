import { Schema } from 'mongoose';

export const UserSchema = new Schema({
  email: { type: String, required: true },
  password: { type: String, required: true },
});

export interface User {
  id: string;
  email: string;
  password: string;
}
