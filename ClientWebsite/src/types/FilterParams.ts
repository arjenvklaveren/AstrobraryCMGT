import { SpaceBodyType } from "./SpaceBody";
import { Field } from "../helpers/decorators/field-decorator";
import { AstronomerOccupation } from "./Astronomer";

export class FilterParams {
    @Field() name?: string;
    @Field() age?: number;
}

export class SpaceBodyFilterParams extends FilterParams {
    @Field({ label: "Type", type: SpaceBodyType }) bodyType?: SpaceBodyType;
    @Field({ label: "Has rings?" }) hasRings?: boolean;
}

export class AstronomerFilterParams extends FilterParams {
    @Field({ label: "Is married?" }) isMarried?: boolean;
    @Field( { type: AstronomerOccupation } ) occupation?: AstronomerOccupation;
}