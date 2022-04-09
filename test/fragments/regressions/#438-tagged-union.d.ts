//--tagged-union

// string kind
declare namespace StringKind {
    interface Circle {
        kind: "circle";
        radius: number;
    }

    interface Square {
        kind: "square_";
        sideLength: number;
    }

    type Shape = Circle | Square;
}

// number kind
declare namespace NumberKind {
    interface Circle {
        kind: 1;
        radius: number;
    }

    interface Square {
        kind: 4.2;
        sideLength: number;
    }

    type Shape = Circle | Square;
}

// mixed kind
declare namespace MixedKind {
    interface Circle {
        kind: 0;
        radius: number;
    }

    interface Square {
        kind: "square!";
        sideLength: number;
    }

    type Shape = Circle | Square;
}

// enum kind
declare namespace EnumKind {
    enum ShapeKind {
        Circle = 1,
        Square = 2,
    }

    interface Circle {
        kind: ShapeKind.Circle;
        radius: number;
    }

    interface Square {
        kind: ShapeKind.Square;
        sideLength: number;
    }

    type Shape = Circle | Square;
}