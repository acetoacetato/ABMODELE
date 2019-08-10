/**
 * `Selection` module is used to handle RTE Selections.
 */
export declare class NodeSelection {
    range: Range;
    rootNode: Node;
    body: HTMLBodyElement;
    html: string;
    startContainer: number[];
    endContainer: number[];
    startOffset: number;
    endOffset: number;
    startNodeName: string[];
    endNodeName: string[];
    private saveInstance;
    private documentFromRange;
    getRange(docElement: Document): Range;
    get(docElement: Document): Selection;
    save(range: Range, docElement: Document): NodeSelection;
    getIndex(node: Node): number;
    private isChildNode;
    private getNode;
    getNodeCollection(range: Range): Node[];
    getParentNodeCollection(range: Range): Node[];
    getParentNodes(nodeCollection: Node[], range: Range): Node[];
    getSelectionNodeCollection(range: Range): Node[];
    getSelectionNodes(nodeCollection: Node[]): Node[];
    getInsertNodeCollection(range: Range): Node[];
    getInsertNodes(nodeCollection: Node[]): Node[];
    getNodeArray(node: Node, isStart: boolean, root?: Document): number[];
    private setRangePoint;
    restore(): Range;
    selectRange(docElement: Document, range: Range): void;
    setRange(docElement: Document, range: Range): void;
    setSelectionText(docElement: Document, startNode: Node, endNode: Node, startIndex: number, endIndex: number): void;
    setSelectionContents(docElement: Document, element: Node): void;
    setSelectionNode(docElement: Document, element: Node): void;
    getSelectedNodes(docElement: Document): Node[];
    Clear(docElement: Document): void;
    insertParentNode(docElement: Document, newNode: Node, range: Range): void;
    setCursorPoint(docElement: Document, element: Element, point: number): void;
}
