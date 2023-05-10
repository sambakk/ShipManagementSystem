/**
 * Check if object values has at least one value
 * @param obj 
 * @returns 
 */
export const hasAtLeastOneValue = (obj: Record<string, any>): boolean => {
    return Object.values(obj).some((value) => value !== '');
};

const getRandomLetter = (): string => {
    const letters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    return letters[Math.floor(Math.random() * letters.length)];
}

const getRandomDigit = (): number => {
    return Math.floor(Math.random() * 10);
}

export const generateRandomString = (): string => {
    const randomLetters = Array.from({ length: 4 }, () => getRandomLetter()).join('');
    const randomDigits = Array.from({ length: 4 }, () => getRandomDigit()).join('');

    return `${randomLetters}-${randomDigits}-${getRandomLetter()}${getRandomDigit()}`;
}