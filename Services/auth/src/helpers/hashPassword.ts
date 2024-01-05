import { genSalt, hash } from 'bcrypt';

const saltRounds = 12;

export async function hashPassword(password: string) {
  try {
    const salt = await genSalt(saltRounds);
    return await hash(password, salt);
  } catch (error) {
    throw error;
  }
}
