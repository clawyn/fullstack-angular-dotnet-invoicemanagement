export interface AllUserResultModel {
  results: UserDtoModel[];
  totalPages: number;
  currentPage: number;
}

export interface UserDtoModel {
  id: number;
  role: string;
  login: string;

}
