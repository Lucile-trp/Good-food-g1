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
import { User } from './users.model';
import { hashPassword } from 'src/helpers/hashPassword';

@Controller('users')
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Get()
  async get(@Query('id') id: string): Promise</*User | User[]*/string> {
    if (id) {
      //const res = await this.userService.getUserWithId(id);
      return 'Hello World !';
    }
    //const res = await this.userService.getAllUsers();
    return 'Hello World !';
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
  async updateUser(
    @Query('id') id: string,
    @Body('email') email?: string,
    @Body('password') password?: string,
  ) {
    const user = await this.userService.getUserWithId(id);

    // Assignation des changements
    email && (user.email = email);
    password && (user.password = await hashPassword(password));

    // Modification de la donnée
    const res = await this.userService.updateUser(id, user);
    return res;
  }

  @Delete()
  async deleteUserQuery(@Query('id') id: string) {
    const res = await this.userService.deleteUser(id);
    return res;
  }
}
