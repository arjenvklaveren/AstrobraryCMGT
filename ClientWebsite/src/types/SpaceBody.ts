import { RingSystem } from "./RingSystem";

export type SpaceBody = {
  id: number | null;
  name: string;
  discoveryDate: string;
  age: number;
  imageUrl: string | null;
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
  ringSystem: RingSystem | null;
  discovererId: number | null;
  children: SpaceBody[];
}

export enum SpaceBodyType {
    Moon,
    Planet,
    Star
}