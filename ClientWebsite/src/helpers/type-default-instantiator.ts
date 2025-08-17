import { Astronomer, AstronomerOccupation } from "../types/Astronomer";
import { RingSystem } from "../types/RingSystem";
import { SpaceBody, SpaceBodyType } from "../types/SpaceBody";

export function getSpaceBodyDefault(): SpaceBody {
    return {
        id: null,
        name: "",
        discoveryDate: "",
        age: 0,
        imageUrl: null,
        parentId: null,
        type: SpaceBodyType.Planet,
        mainInfo: "",
        subInfo: "",
        mass: 0,
        luminosity: 0,
        diameter: 0,
        velocity: 0,
        temperature: 0,
        distanceFromParent: 0,
        rotationAngle: 0,
        rotationSpeed: 0,
        atmosphereThickness: 0,
        mainColorHex: "#000000",
        subColorHex: "#000000",
        ringSystem: null,
        discovererId: null,
        children: []
    }
}

export function getRingSystemDefault(): RingSystem {
    return {
        id: null,
        spaceBodyId: null,
        ringDistance: 0,
        ringSize: 0,
        ringMainColorHex: "#000000",
        ringSubColorHex: "#000000",
        ringDetailColorHex: "#000000",
    }
}

export function getAstronomerDefault(): Astronomer {
    return {
        id: null,
        name: "",
        dateOfBirth: "",
        birthPlace: "",
        imageUrl: null,
        occupation: AstronomerOccupation.Scientist,
        description: "",
        married: false,
        gender: "",
        telescopeAmount: 0
    }
}