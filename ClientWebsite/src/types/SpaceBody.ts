export type SpaceBody = {
  id: number;
  name: string;
  discoveryDate: string;
  age: number;
  imageUrl: string;
  parentId: number | null;
  type: SpaceBodyType;
  mainInfo: string;
  subInfo: string;
  mass: number;
  luminosity: number;
  diameter: number;
  velocity: number;
  temperature: number;
  distanceFromParent: number;
  rotationAngle: number;
  rotationSpeed: number;
  atmosphereThickness: number;
  mainColorHex: string;
  subColorHex: string;
  ringSystem: any;
  discovererId: number;
  discoverer: any;
  children: SpaceBody[];
}

export enum SpaceBodyType {
    Moon,
    Planet,
    Star
}