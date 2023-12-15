import { Injectable } from '@nestjs/common';
import { Model } from 'mongoose';
import { InjectModel } from '@nestjs/mongoose';
import { User } from './users.model';
import { toObjectId } from 'src/helpers/toObjectId';

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
    //TODO : hash password
    const newUser = new this.userModel({
      email,
      password,
    });
    const res = await newUser.save();
    console.log('New user : ', res);
    return res.id;
  }
}
