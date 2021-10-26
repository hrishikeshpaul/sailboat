import axios from 'axios';

export const LS = localStorage;

export interface RedirectResponse {
    accessToken: string;
    refreshToken: string;
    expiresIn: number;
}

export enum HttpStatus {
    OK = 200,
    NO_CONTENT = 204,
    BAD_REQUEST = 400,
    UNAUTHORIZED = 401,
    FORBIDDEN = 403,
    NOT_FOUND = 404,
    INTERNAL_ERROR = 500,
}

export enum TokenStorageKeys {
    AT = 'sb-at',
    RT = 'sb-rt',
    EXP = 'sb-exp',
    TYP = 'sb-typ',
}

export const buildHeaders = (): any => ({
    [TokenStorageKeys.AT]: LS.getItem(TokenStorageKeys.AT) || '',
    [TokenStorageKeys.RT]: LS.getItem(TokenStorageKeys.RT) || '',
    [TokenStorageKeys.EXP]: LS.getItem(TokenStorageKeys.EXP) || '',
});

export const setHeadersToLocalStorage = (
    accessToken: string,
    refreshToken: string,
    exp: number,
    type: string,
): Promise<void> => {
    return new Promise((resolve) => {
        LS.setItem(TokenStorageKeys.AT, accessToken);
        LS.setItem(TokenStorageKeys.RT, refreshToken);
        LS.setItem(TokenStorageKeys.EXP, exp.toString());
        LS.setItem(TokenStorageKeys.TYP, type);
        resolve();
    });
};

export const resetLocalStorage = (): void => {
    LS.removeItem(TokenStorageKeys.AT);
    LS.removeItem(TokenStorageKeys.RT);
    LS.removeItem(TokenStorageKeys.EXP);
};

export const Http = axios.create({
    baseURL: process.env.REACT_APP_BASE_URL,
});
