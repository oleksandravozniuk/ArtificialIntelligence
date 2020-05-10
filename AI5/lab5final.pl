% rbfs ( Path, SiblingNcdes, Bound, NewBestTF, Solved, Solution): 
% h Path - текущий путь, представленный в виде списка узлов в обратном порядке 
% SiblingNodes - дочерние узлы узла в голове списка Path 
% Bound - верхняя граница F-эначения для поиска по узлам списка SibiingModes
% NewBestFF - наилучшее f-эначение после поиска непосредственно за пределами Bound 
% Solved - yes, no или never * Solution - путь решения, если Solved = yes 1 
% Представление узлов: Node - ( State, G/F/FF) % где G - стоимость до наступления состояния State, F - статическое f-значение в состоянии State, FF - резервная копия f-значения в состоянии State


bestfirst(Start, Solution) :-
    rbfs([], [ (Start, 0/0/0) ], 99999, _, yes, Solution).

rbfs(_, [ (_, _/_/FF) | _], Bound, FF, no, _) :-
    FF > Bound, !.

rbfs(Path, [ (Node, _/F/FF) | _], _, _, yes, [Node | Path]) :-                               % Сообщать об успешном решении только один раз, после первого 
    F = FF,                                                                                  % достижения целевого узла; затем F=FF 
    goal(Node).

rbfs(_, [], _, _, never, _) :- !.                                                            % Возможные продолжения отсутствуют, тупиковый конец!

rbfs(Path, [ (Node, G/F/FF) | Ns], Bound, NewFF, Solved, Sol) :-                           
    FF =< Bound,                                                                             % Значение в пределах Bound - сформировать дочерние узлы 
    findall(Child/Cost,(s(Node, Child, Cost), \+ member(Child, Path)),Children),
    inherit(F, FF, InheritedFF),                                                             % Дочерние уэлы могут наследовать значение FF
    succlist(G, InheritedFF, Children, SuccNodes),    % Order children                       % Переупорядочить эти узлы
    bestff(Ns, NextBestFF),                                                                  % Ближайший конкурент по значению FF среди одноранговых узлов 
    min(Bound, NextBestFF, Bound2), !,                                                                              
    rbfs([Node | Path], SuccNodes, Bound2, NewFF2, Solved2, Sol).


succlist(_, _, [], []).

succlist(G0, InheritedFF, [Node/C | NCs], Nodes) :-
    G is G0 + C,
    h(Node, H),
    F is G + H,
    max(F, InheritedFF, FF),
    succlist(G0, InheritedFF, NCs, Nodes2),
    insert((Node, G/F/FF), Nodes2, Nodes).



inherit(F, FF, FF) :-                                                                       % Дочерний узел наследует FF-зкачение родительского  
    FF > F, !.                                                                              % узла, если FF-значение родительского узла больше  F-значения родительского узла

inherit(_, _, 0).



insert((N, G/F/FF), Nodes, [ (N, G/F/FF) | Nodes]) :-
    bestff(Nodes, FF2),
    FF =< FF2, !.

insert(N, [N1 | Ns], [N1 | Ns1]) :-
    insert(N, Ns, Ns1).



bestff([ (_, _/_/FF) | _], FF).                                                             % Первый узел - наилучшее FF-значение

bestff([], 99999).                   % No nodes - FF = "infinite"


min(X, Y, X) :-
    X  =<  Y, !.
min(_, Y, Y).


max(X, Y, X) :-
    X  >=  Y, !.
max(_, Y, Y).




% s( Node, SuccessorNode, Cost)

s( [Empty | Tiles], [Tile | Tiles1], 1)  :-  % Стоимости всех дуг равны 1
  swap( Empty, Tile, Tiles, Tiles1).         % Поменять местами пустую фишку Empty и фишку Tile в списке Tiles

swap( Empty, Tile, [Tile | Ts], [Empty | Ts] )  :-
 mandist( Empty, Tile, 1).                  % Manhattan distance = 1

swap( Empty, Tile, [T1 | Ts], [T1 | Ts1] )  :-
  swap( Empty, Tile, Ts, Ts1).

mandist( X/Y, X1/Y1, D)  :-          % D %  D — манхэттенское расстояние между клетками S1 и S2; оно измеряется как сумма расстояний между S1 и S2 в горизонтальном и вертикальном направлениях. 
  dif( X, X1, Dx),
  dif( Y, Y1, Dy),
  D is Dx + Dy.

dif( A, B, D)  :-              % D is |A-B|
  D is A-B, D >= 0, !
  ;
  D is B-A.

% Heuristic estimate h is the sum of distances of each tile
% from its "home' square plus 3 times "sequence' score

h( [Empty | Tiles], H)  :-
  goal( [Empty1 | GoalSquares] ),
  totdist( Tiles, GoalSquares, D),      
  seq( Tiles, S),                       % Оценка упорядоченности
  H is D +3*S.

  totdist( [], [], 0).

totdist( [Tile | Tiles], [Square | Squares], D)  :-
  mandist( Tile, Square, D1),
  totdist( Tiles, Squares, D2),
  D is D1 + D2.


% seq( TilePositions, Score): sequence score

seq( [First | OtherTiles], S)  :-
  seq( [First | OtherTiles ], First, S).

seq( [Tile1, Tile2 | Tiles], First, S)  :-
  score( Tile1, Tile2, S1),
  seq( [Tile2 | Tiles], First, S2),
  S is S1 + S2.

seq( [Last], First, S)  :-
  score( Last, First, S).

score( 2/2, _, 1)  :-  !.              % Оценка фишки, стоящей в центре, равна 1

score( 1/3, 2/3, 0)  :-  !.            % Оценка фишки, за которой следует допустимый преемник, равна 0
score( 2/3, 3/3, 0)  :-  !.
score( 3/3, 3/2, 0)  :-  !.
score( 3/2, 3/1, 0)  :-  !.
score( 3/1, 2/1, 0)  :-  !.
score( 2/1, 1/1, 0)  :-  !.
score( 1/1, 1/2, 0)  :-  !.
score( 1/2, 1/3, 0)  :-  !.

score( _, _, 2).                       % Оценка фишки, за которой следует недопустимый преемник, равна 2 

goal( [2/2,1/3,2/3,3/3,3/2,3/1,2/1,1/1,1/2] ).  % Goal squares for tiles

% Starting positions for some puzzles

start1( [2/2,1/3,3/2,2/3,3/3,3/1,2/1,1/1,1/2] ).  % Requires 4 steps

% ------------------------------Display a solution path as a list of board positions

showsol( [] ).

showsol( [P | L] )  :-
  showsol( L),
  nl, write( '---'),
  showpos( P).

% Display a board position

showpos( [S0,S1,S2,S3,S4,S5,S6,S7,S8] )  :-
  member( Y, [3,2,1] ),                           % Последовательность координат Y 
  nl, member( X, [1,2,3] ),                       % Последовательность координат х
  member( Tile-X/Y,                               % Фишка в клетке X/Y 
          [' '-S0,1-S1,2-S2,3-S3,4-S4,5-S5,6-S6,7-S7,8-S8] ),
  write( Tile),
  fail                                            % Выполнить перебор с возвратом к следующей клетке 
  ;
  true.                                           %  Обработка всех клеток закончена 






% An example query: ?- start1( Pos), bestfirst( Pos, Sol), showsol( Sol).
















