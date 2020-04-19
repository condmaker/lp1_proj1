# Projeto 1 da Disciplina de Linguagens de Programação 1
## Wolf and Sheep
Marco Domingos, a21901309  
Daniel Fernandes, a21902752

## Autoria 
Nesta secção será indicado exatamente o que cada aluno fez no projeto.

### Classe *'Board'*
Todo o código inicial e os métodos feitos por Daniel Fernandes, com ajustes ao código, comentário e documentação sendo feitas por Marco Domingos. O método ajustado *em código* por Marco Domingos foi este:

- **ShowBoard()**

O resto dos métodos foram codificados por Daniel Fernandes, com comentários/documentação sendo feitas por ambos Daniel e Marco.

### Classe *'Tile'*
Todo o código inicial e os métodos feitos por Daniel Fernandes, com ajustes ao código, comentário e documentação sendo feitas por Marco Domingos. Os métodos ajustados *em código* por Marco Domingos foram estes:

- **PrintTileImage()**  
Substituindo uma mensagem de *debug* no `Console.WriteLine` por uma mensagem de erro
- **CheckTileAvailability()**  
Refazendo minimamente a lógica do código, trocando uma condição de lugar para este funcionar corretamente

O resto dos métodos foram codificados por Daniel Fernandes, com comentários/documentação sendo feitas por ambos Daniel e Marco.

### Classe *'Program'*
Maioritariamente feita por Marco Domingos, com intervenções de Daniel Fernandes.

- **Main()**  
Feito maioritariamente por Marco Domingos, com edições ao meio do desenvolvimento por Daniel Fernandes para implementação experimental da classe *'Board'*.
- **GameStart()**  
Feito parcialmente por Marco Domingos e Daniel Fernandes, com Marco Domingos a estruturar o esqueleto e lógica base do método, e Daniel Fernandes a refinar tal lógica para esta funcionar como deve (como por exemplo, bugs onde ovelhas podiam ir para trás, o lobo se auto-encurralava, etc.)
- **CoordToInt()**  
Feito maioritariamente por Marco Domingos, com Daniel Fernandes dando a fórmula para o cálculo de `xValue` (Isto não está nos commits, pois Daniel Fernandes apenas disse a fórmula e esta foi implementada por Marco Domingos).
- **GameTutorial()**  
Feita inteiramente por Marco Domingos.
- **ContinueText()**  
Feita inteiramente por Marco Domingos.

*[Link para o repositório Github][githublink]*

## Arquitetura da Solução
O programa foi realizado com duas classes principais (não contando a *Program*), *Board* e *Tile*. 

### Classe *Board*
A classe Board irá fazer tudo relacionado ao tabuleiro em sí-- sua construção, a organização de quadrados (chamados de *tiles* no programa, que fazem parte da outra classe *Tile*) no tabuleiro em sí a partir de um array de *Tile*s, a atualização do tabuleiro, e a colocação de *neighbours* para cada um dos tiles.

*Neighbours*, no programa, são as *tiles* adjacentes de uma tile especifica, indicando se uma peça (ovelha ou lobo) pode ir para esta outra *tile*. 

Seguem aqui os métodos específicos desta classe:

- **public Board()**  

É o construtor do método e é chamado quando uma nova instância da classe é criada. Ele irá simplesmente chamar o método `CreateBoard()` e criar um novo tabuleiro para esta instância.

- **public Tile GetTile(int x, int y)**  

Recebe uma coordenada x e y para o tabuleiro 8x8, e retorna um objeto Tile com a posição da coordenada colocada.

- **public void CreateBoard()**  

Recebe um número de lados (porém só funciona com tabuleiros 8x8) e irá construir o tabuleiro em sí, dando vários estados para cada *tile* já definido no *array* de *Tile*s, nomeado de `darkTiles` dentro da classe. Ele faz isto com dois ciclos *for*, que de acordo com suas variáveis de iteração (i e j), irão definir as coordenadas de cada *Tile*, e seu índice (utilizado para acessar o *array* de tiles) será acessado com `tileNumb`, que é incrementado sempre no final do segundo ciclo.  
Ele também verifica a posição inicial das ovelhas no tabuleiro e dá este parâmetro a cada *tile* (com o `isInitialPos`), para verificar depois se o jogador que controla o lobo está numa *tile* inicial da ovelha. Ele no final chama o método `AssignNeighbours()` para dar os parâmetros de *neighbours* a cada *tile*.

- **public void AssignNeighbours()** 
 
De acordo com a colocação da *tile* no tabuleiro, este método irá chamar o outro método, `GetNeighbours()`, para dar os 4 *neighbours* a uma *tile* específica.

- **public void GetNeighbours(int[] typeOfNeighbour, Tile currentTile)**  

Dependendo do tipo de *neighbour* (se são pares ou ímpares), o programa irá dar diferentes valores as variáveis `xInteraction` e `yInteraction`, que irão no final iterar a posição da *tile* dada para encontrar um *neighbour* específico.

- **public int ConvertToArrayNumb(int x, int y)**  

Converte uma posição no tabuleiro (representado por duas ints, x e y) a um índice para ser acessado no *array* de *Tile*s.

- **public void ShowBoard()** 

Imprime e atualiza o tabuleiro principal, utilizando a posição x e y de cada *Tile* para imprimir corretamente.

### Classe *Tile*
A classe *Tile* é feita para cada *tile* em específico, com variáveis e métodos específicados para cada tile. Suas variáveis são:

`isInitialPos`, um booleano que indica se a *tile* é uma posição inicial da ovelha ou não

`tileState`, que indica o estado da *tile* em sí (0 = vazio, 1 = ovelha, 2 = lobo)

`x, y`, as coordenadas numéricas da *tile* no tabuleiro

`index`, uma *int* que indica a posição da *tile* no *array* de *Tile*s no tabuleiro

`neighbours`, um *array* de *Tile*s que irá guardar os *neighbours* desta *tile* em específico;

Para os métodos:

- **public Tile(int x, int y, int state, int index, bool isInitialPos = false)**

É o construtor da classe. Cada instância, quando criada, tem de dar as variáveis da classe já mencionadas acima. Isto é chamado várias vezes no método `CreateBoard()` da classe *Board*.

- **public void PrintTileImage()**

Método conjunto ao `ShowBoard()` da classe *Board*. De acordo com o `tileState` do *Tile* chamado, irá imprimir uma 'imagem' diferente, correspondente com o que está naquele *tile* no momento.

- **public bool CheckTileAvailability(Tile targetTile)**

De acordo com os *neighbours* da *tile*, compara uma *tile* com outra e verifica se o jogador pode mover a peça para a *tile* dada ao método.

- **public bool CheckIfSurrounded()**

Observa os neighbours de uma tile, e se todos eles forem iguais a ovelhas, retorna `true`. Utilizado para ver quando o lobo perde.

### Classe *Program*

#### Main()
O método main é curto, e começa com um símples *loop* de comandos onde de acordo com o *input* do jogador, ele irá entrar outros métodos.

`t` irá chamar o método `GameTutorial()`, que como o nome diz, mostra um tutorial ao jogador.

`s` irá inicializar o jogo entrando no método `GameStart()`. Quando ele retorna à `Main()` após isto, o *loop* será quebrado e o programa finalizado.

`m` imprime o ecrã principal novamente, com o método `MainScreen()`.

`q` irá quebrar o *loop*, finalizando o programa.

Caso o *input* não seja nenhum destes, o jogo irá entrar no *case default* e imprimir que não reconhece o input, retornando ao início do loop.

- **private static void MainScreen()**

Simplesmente imprime o ecrã principal enquanto chamado em `Main()`.

- **private static void GameStart()**

É onde o jogo em sí acontece. Faz-se com um loop que alterna entre turnos pares e ímpares para decidir se é a jogada do lobo ou das ovelhas. Pode-se ver melhor no [Fluxograma da Solução](#fluxograma-da-solu%c3%a7%c3%a3o).

- **private static int[] CoordToInt(char a, char b)**

Método que transforma dois *char*s em um *array* de *int*s. É utilizado para converter *inputs* do jogador em coordenadas, de forma que o programa acesse a *tile* desejada. Utilizado em `GameStart()`.

- **private static void GameTutorial()**

Cria um novo objeto da classe *Board* para demonstração, e entra num loop que imprime texto, equivalente ao tutorial do jogo. O tutorial é dividido em secções, e no fim de uma secção, é chamado outro método, `ContinueText()`, para observar se o jogador quer continuar com o tutorial ou sair do mesmo. 

- **private static bool ContinueText()**

Símples método que imprime um texto e verifica o *input* de um jogador, retornando um booleano. É feito para não repetir código no método `GameTutorial()`.

### Fluxograma da Solução
![fluxograma]

[githublink]: https://github.com/condmaker/lp1_proj1
[fluxograma]: Diagrams/Fluxograma.png