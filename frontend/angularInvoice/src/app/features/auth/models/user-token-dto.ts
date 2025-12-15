export interface UserTokenDto {
  accessToken: string;
  user: UserSessionDto;
}

export interface UserSessionDto {
  id: string;
  login: string;
  email: string;
}
