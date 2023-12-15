import { Injectable } from '@nestjs/common';
import { Model } from 'mongoose';
import { InjectModel } from '@nestjs/mongoose';
import { User } from './users.model';
import { toObjectId } from 'src/helpers/toObjectId';
import { hashPassword } from 'src/helpers/hashPassword';

@Injectable()
export class UserService {
  constructor(@InjectModel('User') private readonly userModel: Model<User>) {}

  async getAllUsers() {
    const users = this.userModel.find();
    return users;
  }

  async getUserWithId(id: string) {
    const objectId = toObjectId(id);
    const user = this.userModel.find({ _id: objectId });
    return user;
  }

  async insertUser(email: string, password: string) {
    const passwordHash = await hashPassword(password);
    const newUser = new this.userModel({
      email,
      password: passwordHash,
    });
    const res = await newUser.save();
    return res.id;
  }

  // TODO : update

  async deleteUser(id: string) {
    const objectId = toObjectId(id);
    const res = await this.userModel.findByIdAndDelete(objectId);
    return res;
  }
}
