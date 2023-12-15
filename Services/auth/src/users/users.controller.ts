import {
  Body,
  Controller,
  Delete,
  Get,
  Post,
  Put,
  Query,
} from '@nestjs/common';
import { UserService } from './users.service';

@Controller('users')
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Get()
  async get(@Query('id') id: string) {
    if (id) {
      const res = await this.userService.getUserWithId(id);
      return res;
    }
    const res = await this.userService.getAllUsers();
    return res;
  }

  @Post()
  async insertUser(
    @Body('email') email: string,
    @Body('password') password: string,
  ): Promise<string> {
    const res = await this.userService.insertUser(email, password);
    return res;
  }

  @Put()
  async updateUser() {
    // TODO
  }

  @Delete()
  async deleteUserQuery(@Query('id') id: string) {
    const res = await this.userService.deleteUser(id);
    return res;
  }
}
