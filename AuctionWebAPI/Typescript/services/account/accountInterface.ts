
export interface IAccount {
  AccountId: number;
  Username: string;
  Password: string;
}

export interface IAccountDTO {
    AccountId: number;
    Username: string;
}

export interface ICreateAccountInput {
    Username: string;
    Password: string;
    PasswordConfirmation: string;
}

export interface ICreateAccountOutput {
    Username: string;
    AuthToken: string;
}

export interface ILoginAccountInput {
    Username: string;
    Password: string;
}

export interface ILoginAccountOutput {
    Username: string;
    AuthToken: string;
}

export interface IDeleteAccountInput {
  AccountId: number;
}

export interface IDeleteAccountOutput {
  AccountId: number;
}
