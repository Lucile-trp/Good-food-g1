import { Schema } from 'mongoose';

export enum AllowedRoles {
  ADMIN = 'ADMIN',
  FRANCHISE = 'FRANCHISE',
  DELIVERER = 'DELIVERER',
  USER = 'USER',
}

export const UserSchema = new Schema({
  email: { type: String, required: true },
  password: { type: String, required: true },
  role: { 
    type: String, 
    enum: Object.values(AllowedRoles),
    default: AllowedRoles.USER 
  }
});

export interface User {
  _id: string;
  email: string;
  password: string;
  role: AllowedRoles;
}
