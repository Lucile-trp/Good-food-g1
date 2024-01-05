import { Injectable, NotFoundException } from '@nestjs/common';
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
    if (!users) {
      throw new NotFoundException('Something bad happened');
    }
    return users;
  }

  async getUserWithId(id: string) {
    const objectId = toObjectId(id);
    const user = this.userModel.findOne({ _id: objectId });
    if (!user) {
      throw new NotFoundException('User not found');
    }
    return user;
  }

  async getUserByEmail(email: string) {
    const user = this.userModel.findOne({ email: email });
    if (!user) {
      throw new NotFoundException('User not found');
    }
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

  async updateUser(id: string, user: User) {
    const objectId = toObjectId(id);
    const res = await this.userModel.findOneAndUpdate({ _id: objectId }, user, {
      returnDocument: 'after',
    });
    if (!res) {
      throw new NotFoundException('User not found');
    }
    return res;
  }

  async deleteUser(id: string) {
    const objectId = toObjectId(id);
    const res = await this.userModel.findByIdAndDelete(objectId);
    if (!res) {
      throw new NotFoundException('User not found');
    }
    return res;
  }
}
