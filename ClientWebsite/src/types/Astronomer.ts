export type Astronomer = {
  id: number;
  name: string;
  dateOfBirth: string;
  birthPlace: string;
  imageUrl: string;
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
