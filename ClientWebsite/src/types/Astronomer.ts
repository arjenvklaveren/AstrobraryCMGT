export type Astronomer = {
  id?: number | null;
  name: string;
  dateOfBirth: string;
  birthPlace: string;
  imageUrl: string | null;
  occupation: AstronomerOccupation;
  description: string;
  married: boolean;
  gender: string;
  telescopeAmount: number;
}

export enum AstronomerOccupation
{
    Scientist,
    Hobbyist,
    Astronaut,
    Researcher
}
