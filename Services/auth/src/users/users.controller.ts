import { Body, Controller, Get, Param, Post } from '@nestjs/common';
import { UserService } from './users.service';

@Controller('users')
export class UserController {
  constructor(private readonly userService: UserService) {}

  @Get()
  async getAll() {
    const res = await this.userService.getAllUsers();
    return res;
  }

  @Get(':id')
  async getUserWithId(@Param('id') id: string) {
    const res = await this.userService.getUserWithId(id);
    return res;
  }

  @Post()
  async insertUser(
    @Body('email') email: string,
    @Body('password') password: string,
  ): Promise<string> {
    const res = await this.userService.insertUser(email, password);
    console.log(res);
    return res;
  }
}
